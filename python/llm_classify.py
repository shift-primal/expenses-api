import json
import anthropic
from categories import CATEGORIES

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
    block = message.content[0]
    assert hasattr(block, "text")
    text = block.text
    # Extract JSON array from response
    start = text.index("[")
    end = text.rindex("]") + 1
    results = json.loads(text[start:end])
    return [(r["butikk"], r["kategori"]) for r in results]
