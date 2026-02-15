import pandas as pd
import json
from categories import MERCHANT_RULES
from llm_classify import classify_with_llm


print(MERCHANT_RULES)

file_path = './data/kasper/transactions.txt'

df = pd.read_csv(file_path, sep=";", decimal=",")

df = df.drop(columns=["Rentedato"])

df["Ut fra konto"] = pd.to_numeric(df["Ut fra konto"], errors="coerce")
df["Inn på konto"] = pd.to_numeric(df["Inn på konto"], errors="coerce")

df["Beløp"] = df["Inn på konto"] - df["Ut fra konto"]
df["Innskudd"] = df["Inn på konto"] > 0

df = df.drop(columns=["Ut fra konto", "Inn på konto"])

def match_rule(forklaring: str) -> tuple[str, str] | None:
    forklaring_lower = forklaring.lower()
    for keyword, (merchant, category) in MERCHANT_RULES.items():
        if keyword.lower() in forklaring_lower:
            return merchant, category
    return None

merchants = []
categories = []
unmatched_indices = []

for i, row in df.iterrows():
    result = match_rule(str(row["Forklaring"]))
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
