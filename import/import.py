import csv
import json

file_path = './transactions.txt'



with open(file_path) as f:
    reader = csv.DictReader(f, delimiter=';')
    transactions = [
        {k: v.strip() for k, v in row.items()} for row in reader
    ]

def transaction_parser(t):
    try:
        return t.get("Forklaring").split()
    except Exception as e:
        print("Exception:", e)

def get_transaction_type(t):
    r = transaction_parser(t)

    if r != None:
        return r[0]

for i, t in enumerate(transactions):
    type = get_transaction_type(t)
    print(type)
