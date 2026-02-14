import json
from collections import Counter

file_path = './data/kasper/kvitteringer_trumf.json'

with open(file_path, 'r') as f:
    data = json.load(f)

counts = Counter()

for k in data:
    for v in k["varelinjer"]:
        name = v["varenavn"].upper()
        if "MONSTER" in name:
            counts[v["varenavn"]] += int(float(v["vareAntallVekt"]))

        # for item, count in counts.most_common():
        #     print(f"{item}: {count}")

print(counts)
