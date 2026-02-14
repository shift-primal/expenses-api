import pandas as pd
import anthropic
import json
import "./categories.py" as categories

file_path = './transactions.txt'

df = pd.read_csv(file_path, sep=";", decimal=",")

df = df.drop(columns=["Rentedato"])

df["Ut fra konto"] = pd.to_numeric(df["Ut fra konto"], errors="coerce")
df["Inn på konto"] = pd.to_numeric(df["Inn på konto"], errors="coerce")

df["Beløp"] = df["Inn på konto"] - df["Ut fra konto"]
df["Innskudd"] = df["Inn på konto"] > 0

df = df.drop(columns=["Ut fra konto", "Inn på konto"])

# --- Merchant & Category extraction ---




def match_rule(forklaring: str) -> tuple[str, str] | None:
    forklaring_lower = forklaring.lower()
    for keyword, (merchant, category) in MERCHANT_RULES.items():
        if keyword.lower() in forklaring_lower:
            return merchant, category
    return None


def classify_with_llm(descriptions: list[str]) -> list[tuple[str, str]]:
    client = anthropic.Anthropic()
    prompt = (
        "Du får en liste med transaksjonsforklaringer fra en norsk bank.\n"
        "For hver linje, ekstraher butikknavnet og velg én kategori fra denne listen:\n"
        f"{', '.join(CATEGORIES)}\n\n"
        "Svar BARE med en JSON-array med objekter: "
        '[{"butikk": "...", "kategori": "..."}]\n'
        "Én per linje, i samme rekkefølge.\n\n"
        "Transaksjoner:\n"
    )
    for i, desc in enumerate(descriptions, 1):
        prompt += f"{i}. {desc}\n"

    message = client.messages.create(
        model="claude-sonnet-4-5-20250929",
        max_tokens=1024,
        messages=[{"role": "user", "content": prompt}],
    )
    text = message.content[0].text
    # Extract JSON array from response
    start = text.index("[")
    end = text.rindex("]") + 1
    results = json.loads(text[start:end])
    return [(r["butikk"], r["kategori"]) for r in results]


# Apply rule-based matching
merchants = []
categories = []
unmatched_indices = []

for i, row in df.iterrows():
    result = match_rule(row["Forklaring"])
    if result:
        merchants.append(result[0])
        categories.append(result[1])
    else:
        merchants.append(None)
        categories.append(None)
        unmatched_indices.append(i)

# LLM fallback for unmatched rows
if unmatched_indices:
    unmatched_descriptions = [df.at[i, "Forklaring"] for i in unmatched_indices]
    print(f"Klassifiserer {len(unmatched_indices)} transaksjoner med Claude...")
    llm_results = classify_with_llm(unmatched_descriptions)
    for idx, (merchant, category) in zip(unmatched_indices, llm_results):
        merchants[idx] = merchant
        categories[idx] = category

df["Butikk"] = merchants
df["Kategori"] = categories

print(df.head(20))
