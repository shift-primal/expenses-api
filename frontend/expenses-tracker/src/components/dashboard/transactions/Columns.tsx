import { fmtCurrency, getCategoryName } from "@/lib/chartUtils";
import type { Transaction } from "@/types";
import type { ColumnDef } from "@tanstack/react-table";

export const Columns: ColumnDef<Transaction>[] = [
  {
    accessorKey: "category",
    header: "Kategori",
    cell: ({ row }) => {
      const categoryName = getCategoryName(row.getValue("category"));
      return <div className="font-medium">{categoryName}</div>;
    }
  },
  {
    accessorKey: "date",
    header: "Dato"
  },
  {
    accessorKey: "merchant",
    header: "Forhandler"
  },
  {
    accessorKey: "amount",
    header: () => <div className="text-right">Mengde</div>,
    cell: ({ row }) => {
      const formatted = fmtCurrency(row.getValue("amount"));
      return <div className="text-right font-medium">{formatted}</div>;
    }
  }
];
