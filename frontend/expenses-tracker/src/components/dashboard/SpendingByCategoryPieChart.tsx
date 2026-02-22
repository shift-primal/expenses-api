import { TrendingUp } from "lucide-react";
import { Pie, PieChart } from "recharts";

import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle
} from "@/components/shadcn/ui/card";
import {
  ChartContainer,
  ChartTooltip,
  ChartTooltipContent,
  type ChartConfig
} from "@/components/shadcn/ui/chart";
import type { CategorySummary } from "@/types";

export const SpendingByCategoryPieChart = ({
  data
}: {
  data: CategorySummary[];
}) => {
  console.log(data);

  const chartData = data.map((c, i) => ({
    name: c.category.name,
    value: Math.abs(c.total),
    fill: `var(--chart-${(i % 5) + 1})`
  }));

  const chartConfig = Object.fromEntries(
    data.map((c) => [c.category.name, { label: c.category.name }])
  ) satisfies ChartConfig;

  console.log(chartData);

  return (
    <Card className="flex flex-col">
      <CardHeader className="items-center pb-0">
        <CardTitle>Kategori</CardTitle>
        <CardDescription>January - June 2024</CardDescription>
      </CardHeader>
      <CardContent className="flex-1 pb-0">
        <ChartContainer
          config={chartConfig}
          className="mx-auto aspect-square max-h-62.5"
        >
          <PieChart>
            <ChartTooltip
              cursor={false}
              content={<ChartTooltipContent hideLabel />}
            />
            <Pie data={chartData} dataKey="value" nameKey="name" />
          </PieChart>
        </ChartContainer>
      </CardContent>
      <CardFooter className="flex-col gap-2 text-sm">
        <div className="flex items-center gap-2 leading-none font-medium">
          <span>
            Du brukte mest penger på{" "}
            {data.reduce((a, b) => (a.total > b.total ? a : b)).category.name}
          </span>
          <TrendingUp className="h-4 w-4" />
        </div>
        <div className="text-muted-foreground leading-none">
          <span>
            Over {Math.max(...data.map((o) => o.count))} transaksjoner, brukte
            du {Math.max(...data.map((o) => Math.abs(o.total)))}kr
          </span>
        </div>
      </CardFooter>
    </Card>
  );
};
