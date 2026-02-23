import { TrendingUp } from "lucide-react";
import { Area, AreaChart, CartesianGrid, XAxis } from "recharts";

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
import type { MonthSummary } from "@/types";
import { getDateRange, getMostExpensiveMonth } from "@/lib/utils";

export const ByMonthChart = ({ data }: { data: MonthSummary[] }) => {
  const chartData = data.map((s) => ({
    date: Date.parse(s.date),
    value: Math.abs(s.total)
  }));

  const chartConfig = Object.fromEntries(
    data.map((c) => [c.date, { label: c.date, color: "var(--chart-1)" }])
  ) satisfies ChartConfig;

  const { min, max } = getDateRange(data);
  const { expensiveMonthDate, expensiveMonthTotal } =
    getMostExpensiveMonth(data);

  return (
    <Card>
      <CardHeader>
        <CardTitle>Area Chart - Linear</CardTitle>
        <CardDescription>
          Showing total visitors for the last 6 months
        </CardDescription>
      </CardHeader>
      <CardContent>
        <ChartContainer config={chartConfig}>
          <AreaChart
            accessibilityLayer
            data={chartData}
            margin={{
              left: 12,
              right: 12
            }}
          >
            <CartesianGrid vertical={false} />
            <XAxis
              dataKey="date"
              tickLine={false}
              axisLine={false}
              tickMargin={8}
              tickFormatter={(date) =>
                new Date(date).toLocaleDateString("nb-NO", { month: "short" })
              }
            />
            <ChartTooltip
              cursor={false}
              content={
                <ChartTooltipContent
                  indicator="dot"
                  formatter={(value) => `kr. Brukt: ${value}`}
                  hideLabel
                />
              }
            />
            <Area dataKey="value" type="linear" fillOpacity={0} />
          </AreaChart>
        </ChartContainer>
      </CardContent>
      <CardFooter>
        <div className="flex w-full items-start gap-2 text-sm">
          <div className="grid gap-2">
            <div className="flex items-center gap-2 leading-none font-medium">
              <span>
                {`Din dyreste mnd var ${expensiveMonthDate}, med ${expensiveMonthTotal}kr`}
                <TrendingUp className="h-4 w-4" />
              </span>
            </div>
            <div className="text-muted-foreground flex items-center gap-2 leading-none">
              <span>
                Fra {min} til {max}
              </span>
            </div>
          </div>
        </div>
      </CardFooter>
    </Card>
  );
};
