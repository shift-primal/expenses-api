import { PageLayout } from "@layout/PageLayout";
import { useTransactions } from "@hooks/useTransaction";
import { TransactionsTable } from "@/components/dashboard/transactions/TransactionTable";
import { Columns } from "@components/dashboard/transactions/Columns";
import { useState } from "react";
import { type TransactionFilters } from "@types";
import { Button } from "@base-ui/react";

export const TableTest = () => {
  const [page, setPage] = useState(0);
  const [filters, setFilters] = useState<TransactionFilters>({});

  const { data: transactions, isLoading: transactionsLoading } =
    useTransactions(page, { filters });

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

        <div className="flex flex-col items-center">
          <span>{page + 1}</span>
          <div className="flex items-center justify-center gap-8 space-x-2 py-4">
            <Button
              variant="outline"
              size="sm"
              onClick={() => setPage((p) => p - 1)}
              disabled={page == 0}
            >
              Previous
            </Button>
            <Button
              variant="outline"
              size="sm"
              onClick={() => setPage((p) => p + 1)}
              disabled={transactions.length < 25}
            >
              Next
            </Button>
          </div>{" "}
        </div>
      </div>
    </PageLayout>
  );
};
