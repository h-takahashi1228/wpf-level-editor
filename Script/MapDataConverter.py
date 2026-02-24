import json
import csv
import sys
import os

def convert(json_path):
    # JSON 読み込み
    with open(json_path, "r", encoding="utf-8") as f:
        data = json.load(f)

    blocks = data.get("Blocks", [])

    # 出力ファイル名
    base, _ = os.path.splitext(json_path)
    csv_path = base + ".csv"

    # CSV 書き込み
    with open(csv_path, "w", newline="", encoding="utf-8") as f:
        writer = csv.writer(f)
        writer.writerow(["x", "y", "z", "shape", "type", "yaw"])

        for b in blocks:
            pos = b["Position"]
            writer.writerow([
                pos["X"],
                pos["Y"],
                pos["Z"],
                b["Shape"],
                b["Type"],
                b["Yaw"]
            ])

    print(f"CSV exported: {csv_path}")


if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: python convert_json_to_csv.py <json_path>")
        sys.exit(1)

    convert(sys.argv[1])
