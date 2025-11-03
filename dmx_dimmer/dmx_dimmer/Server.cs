using dmx_dimmer;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MiniWebPanel
{
    public class Server
    {
        private readonly HttpListener _listener = new();
        private CancellationTokenSource? _cts;
        private readonly object _lock = new();
        private CanvasState _state = new();
        public bool IsRunning => _listener.IsListening;
        public string? Password { get; set; }   // wenn gesetzt, wird einfacher Schutz aktiv

        public void SetState(CanvasState state)
        {
            lock (_lock) { _state = state; }
        }

        public void Start(string prefix = "http://+:8080/")
        {
            if (IsRunning) return;
            _listener.Prefixes.Clear();
            _listener.Prefixes.Add(prefix);
            _listener.Start();
            _cts = new CancellationTokenSource();
            _ = Task.Run(() => AcceptLoop(_cts.Token));
        }

        public void Stop()
        {
            try { _cts?.Cancel(); } catch { }
            if (IsRunning) _listener.Stop();
        }

        private async Task AcceptLoop(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                HttpListenerContext ctx;
                try { ctx = await _listener.GetContextAsync(); }
                catch when (ct.IsCancellationRequested) { break; }
                catch { continue; }

                _ = Task.Run(() => Handle(ctx), ct);
            }
        }

        private void Handle(HttpListenerContext ctx)
        {
            try
            {
                // Einfacher Passwortschutz: "X-Panel-Password: <pw>"
                if (!string.IsNullOrEmpty(Password))
                {
                    var hdr = ctx.Request.Headers["X-Panel-Password"];
                    if (!string.Equals(hdr, Password, StringComparison.Ordinal))
                    {
                        ctx.Response.StatusCode = 401;
                        Write(ctx, "Unauthorized – set header X-Panel-Password", "text/plain");
                        return;
                    }
                }

                var path = ctx.Request.Url!.AbsolutePath.ToLowerInvariant();

                if (path == "/" || path == "/index.html")
                {
                    CanvasState snap;
                    lock (_lock) snap = _state;
                    var html = RenderHtml(snap);
                    Write(ctx, html, "text/html; charset=utf-8");
                }
                else if (path == "/state.json")
                {
                    CanvasState snap;
                    lock (_lock) snap = _state;
                    var json = JsonSerializer.Serialize(snap, new JsonSerializerOptions { WriteIndented = true });
                    Write(ctx, json, "application/json; charset=utf-8");
                }
                else if (path == "/api/do")
                {
                    var id = ctx.Request.QueryString["id"];
                    // TODO: hier könntest du Aktionen triggern (DMX, TCP, usw.)
                    Write(ctx, $"OK (id={id})", "text/plain; charset=utf-8");
                }
                else
                {
                    ctx.Response.StatusCode = 404;
                    Write(ctx, "Not Found", "text/plain; charset=utf-8");
                }
            }
            catch
            {
                try { ctx.Response.StatusCode = 500; Write(ctx, "Server error", "text/plain"); } catch { }
            }
            finally
            {
                try { ctx.Response.OutputStream.Close(); } catch { }
            }
        }

        private static void Write(HttpListenerContext ctx, string body, string contentType)
        {
            var bytes = Encoding.UTF8.GetBytes(body);
            ctx.Response.ContentType = contentType;
            ctx.Response.ContentLength64 = bytes.Length;
            ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
        }

        private static string RenderHtml(CanvasState state)
        {
            // minimales HTML + Inline-CSS
            var sb = new StringBuilder();
            sb.Append("""
<!doctype html><html><head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width,initial-scale=1">
<title>
""");
            sb.Append(WebUtility.HtmlEncode(state.Title ?? "Panel"));
            sb.Append("""
</title>
<style>
body{font-family:system-ui,-apple-system,Segoe UI,Roboto,Arial;margin:0;background:#f5f5f5}
.header{padding:12px 16px;background:#222;color:#fff;font-weight:600}
.canvas{position:relative;height:calc(100vh - 52px);background:#fff;margin:12px;border:1px solid #ddd;border-radius:8px;overflow:hidden}
.btn{position:absolute;border:0;padding:8px 12px;border-radius:8px;box-shadow:0 1px 3px rgba(0,0,0,.2);cursor:pointer}
.btn:active{transform:scale(.98)}
</style>
</head><body>
<div class="header">
""");
            sb.Append(WebUtility.HtmlEncode(state.Title ?? "Panel"));
            sb.Append("</div><div class=\"canvas\">");

            foreach (var w in state.Widgets)
            {
                sb.Append($"<button class=\"btn\" style=\"left:{w.X}px;top:{w.Y}px;width:{w.Width}px;height:{w.Height}px\" ");
                sb.Append($"onclick=\"fetch('/api/do?id={WebUtility.UrlEncode(w.Id)}').then(()=>console.log('ok'))\">");
                sb.Append(WebUtility.HtmlEncode(w.Text));
                sb.Append("</button>");
            }

            sb.Append("</div></body></html>");
            return sb.ToString();
        }
    }
}
