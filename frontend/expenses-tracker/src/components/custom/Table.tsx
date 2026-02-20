import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableHead,
  TableHeader,
  TableRow
} from "@/components/ui/table";
import type { Transaction } from "@/types";

const transactionRows = (transactions: Transaction[]) => {
  return transactions.map((t) => (
    <TableRow
      key={t.id}
      className={Number(t.amount) < 0 ? "bg-red-400" : "bg-green-400"}
    >
      <TableCell className="font-medium">{t.id}</TableCell>
      <TableCell>{t.category?.name}</TableCell>
      <TableCell>{t.date}</TableCell>
      <TableCell>{t.amount}</TableCell>
    </TableRow>
  ));
};

export const CustomTable = ({
  transactions
}: {
  transactions: Transaction[];
}) => {
  return (
    <Table>
      <TableCaption>HAH!!!</TableCaption>
      <TableHeader>
        <TableRow>
          <TableHead className="w-25">Id</TableHead>
          <TableHead>Kategori</TableHead>
          <TableHead>Dato</TableHead>
          <TableHead>Amount</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>{transactionRows(transactions)}</TableBody>
    </Table>
  );
};
