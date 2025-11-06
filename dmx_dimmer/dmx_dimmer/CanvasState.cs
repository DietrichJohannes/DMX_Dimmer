using System.Collections.Generic;

namespace dmx_dimmer
{
    public class CanvasState
    {
        public List<Widget> Widgets { get; set; } = new();
        public string? Title { get; set; } = "DXM_DIMMER";
    }

    public class Widget
    {
        public string Id { get; set; } = System.Guid.NewGuid().ToString("N");
        public string Text { get; set; } = "Button";
        public int X { get; set; } = 50;
        public int Y { get; set; } = 50;
        public int Width { get; set; } = 100;
        public int Height { get; set; } = 36;
        public string Action { get; set; } = "#";
    }
}