import { PageLayout } from "@layout/PageLayout";
import { useTransactions } from "@hooks/useTransaction";
import { TransactionsTable } from "@/components/dashboard/transactions/TransactionTable";
import { Columns } from "@components/dashboard/transactions/Columns";

export const TableTest = () => {
  const { data: transactions, isLoading: transactionsLoading } =
    useTransactions(0);

  const isLoading = transactionsLoading || transactions?.length == 0;

  if (isLoading || !transactions)
    return (
      <PageLayout>
        <h1>Loading...</h1>
      </PageLayout>
    );

  return (
    <PageLayout>
      <div className="flex flex-1 flex-col">
        <div className="@container/main flex flex-1 flex-col gap-2 p-4">
          <TransactionsTable data={transactions} columns={Columns} />
        </div>
      </div>
    </PageLayout>
  );
};
