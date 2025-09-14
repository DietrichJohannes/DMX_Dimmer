#!/usr/bin/env python3
# artnet_receiver.py
import socket
import struct
import argparse
import datetime

ARTNET_PORT = 6454
ARTNET_ID = b'Art-Net\x00'

# OpCodes (Little Endian)
OP_POLL   = 0x2000  # ArtPoll
OP_DMX    = 0x5000  # ArtDmx
OP_POLL_REPLY = 0x2100
OP_SYNC   = 0x5200

def hexdump(data: bytes, width: int = 16) -> str:
    lines = []
    for i in range(0, len(data), width):
        chunk = data[i:i+width]
        hexpart = ' '.join(f'{b:02X}' for b in chunk)
        ascii_part = ''.join(chr(b) if 32 <= b < 127 else '.' for b in chunk)
        lines.append(f'{i:04X}  {hexpart:<{width*3}}  {ascii_part}')
    return '\n'.join(lines)

def parse_artdmx(payload: bytes):
    """
    ArtDmx packet structure (after 8-byte ID and 2-byte OpCode):
    ProtVerHi (1), ProtVerLo (1),
    Sequence (1), Physical (1),
    SubUni (1), Net (1),
    LengthHi (1), LengthLo (1),
    Data (n)
    """
    if len(payload) < 10:
        return None, "Payload too short for ArtDmx"
    prot_hi, prot_lo, seq, phys, subuni, net, len_hi, len_lo = struct.unpack('>BBBBBBBB', payload[:8])
    dlen = (len_hi << 8) | len_lo
    data = payload[8:8+dlen]
    if len(data) != dlen:
        return None, f"Data length mismatch: header says {dlen}, got {len(data)}"
    # SubUni: high nibble = Universe (0-15), low nibble = Sub-Subnet (0-15)
    univ_hi = (subuni >> 4) & 0x0F
    univ_lo = subuni & 0x0F
    # Common absolute universe number as Net*256 + SubUniByte (used by viele Tools)
    absolute_universe = net * 256 + subuni
    parsed = {
        "prot_ver": (prot_hi, prot_lo),
        "sequence": seq,
        "physical": phys,
        "net": net,
        "universe_hi": univ_hi,
        "subuni_lo": univ_lo,
        "subuni_byte": subuni,
        "absolute_universe": absolute_universe,
        "length": dlen,
        "data": data
    }
    return parsed, None

def main():
    ap = argparse.ArgumentParser(description="Simple Art-Net receiver (ArtDmx logger).")
    ap.add_argument("--bind", default="0.0.0.0", help="IP-Adresse zum Binden (Standard: 0.0.0.0)")
    ap.add_argument("--port", type=int, default=ARTNET_PORT, help="UDP-Port (Standard: 6454)")
    ap.add_argument("--filter-net", type=int, help="Nur Pakete mit diesem Net anzeigen (0-127)")
    ap.add_argument("--filter-subuni", type=lambda x: int(x, 0), help="Nur Pakete mit diesem SubUni-Byte (0x00-0xFF oder 0-255)")
    ap.add_argument("--filter-abs", type=int, help="Nur Pakete mit absolutem Universe (Net*256 + SubUni)")
    ap.add_argument("--print-channels", type=int, default=16, help="Wieviele Kanäle zeigen (0=keine, -1=alle, Standard: 16)")
    ap.add_argument("--hexdump", action="store_true", help="Rohdaten als Hexdump ausgeben")
    args = ap.parse_args()

    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    # Empfehlenswert für Broadcast-Empfang
    sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    try:
        sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
    except OSError:
        pass

    sock.bind((args.bind, args.port))
    print(f"[+] Listening on {args.bind}:{args.port} (Art-Net)…")

    while True:
        data, addr = sock.recvfrom(2048 + 32)  # DMX max 512 Bytes, Header ~18 Bytes, Reserve
        now = datetime.datetime.now().strftime("%H:%M:%S")

        # Mindestlänge + ID prüfen
        if len(data) < 10 or not data.startswith(ARTNET_ID):
            # Kein Art-Net, ggf. ignorieren
            continue

        # OpCode (LE)
        op_code = struct.unpack('<H', data[8:10])[0]
        payload = data[10:]

        if op_code == OP_DMX:
            parsed, err = parse_artdmx(payload)
            if err:
                print(f"[{now}] ArtDmx from {addr[0]}:{addr[1]} - ERROR: {err}")
                continue

            # Filter
            if args.filter_net is not None and parsed["net"] != args.filter_net:
                continue
            if args.filter_subuni is not None and parsed["subuni_byte"] != args.filter_subuni:
                continue
            if args.filter_abs is not None and parsed["absolute_universe"] != args.filter_abs:
                continue

            # Ausgabe
            d = parsed["data"]
            show_n = parsed["length"] if args.print_channels == -1 else max(0, args.print_channels)
            first_vals = d[:show_n] if show_n > 0 else b''
            first_vals_str = ' '.join(f'{b:3d}' for b in first_vals)

            print(
                f"[{now}] ArtDmx {addr[0]}  "
                f"Net={parsed['net']}  Univ={parsed['universe_hi']}  SubUni={parsed['subuni_lo']} "
                f"(SubUniByte=0x{parsed['subuni_byte']:02X}, Abs={parsed['absolute_universe']})  "
                f"Seq={parsed['sequence']}  Phys={parsed['physical']}  Len={parsed['length']}"
            )
            if show_n != 0:
                print(f"       Ch1..{len(first_vals)}: {first_vals_str}")
            if args.hexdump:
                print(hexdump(d))

        elif op_code == OP_POLL:
            print(f"[{now}] ArtPoll from {addr[0]}:{addr[1]}")
        elif op_code == OP_POLL_REPLY:
            print(f"[{now}] ArtPollReply from {addr[0]}:{addr[1]} ({len(payload)} bytes)")
        elif op_code == OP_SYNC:
            print(f"[{now}] ArtSync from {addr[0]}:{addr[1]}")
        else:
            print(f"[{now}] Other Art-Net OpCode=0x{op_code:04X} from {addr[0]}:{addr[1]} ({len(payload)} bytes)")

if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print("\nBye.")
