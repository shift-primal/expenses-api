import { TrendingUp } from "lucide-react";
import { Area, AreaChart, CartesianGrid, XAxis } from "recharts";

import {
  Card,
  CardAction,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle
} from "@shadcn/ui/card";
import {
  ChartContainer,
  ChartTooltip,
  ChartTooltipContent,
  type ChartConfig
} from "@shadcn/ui/chart";
import type { MonthSummary } from "@types";
import {
  fmtCurrency,
  getDateRange,
  getMostExpensiveMonth
} from "@lib/chartUtils";
import { useIsMobile } from "@hooks/useIsMobile";
import { useEffect, useState } from "react";
import { ToggleGroup, ToggleGroupItem } from "@shadcn/ui/toggle-group";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from "@shadcn/ui/select";

export const ByMonthChart = ({ data }: { data: MonthSummary[] }) => {
  const chartData = data.map((s) => ({
    date: Date.parse(`${s.date}-01`),
    value: Math.abs(s.total)
  }));

  const chartConfig = Object.fromEntries(
    data.map((c) => [c.date, { label: c.date, color: "var(--chart-1)" }])
  ) satisfies ChartConfig;

  const { min, max, diffInDays } = getDateRange(data);
  const { expensiveMonthDate, expensiveMonthTotal } =
    getMostExpensiveMonth(data);

  const isMobile = useIsMobile();
  const [timeRange, setTimeRange] = useState(`${diffInDays}d`);

  useEffect(() => {
    if (isMobile) {
      setTimeRange("7d");
    }
  }, [isMobile]);

  return (
    <Card className="@container/card">
      <CardHeader>
        <CardTitle>Pr. Måned</CardTitle>
        <CardDescription>
          <span>
            {min} - {max}
          </span>
        </CardDescription>
        <CardAction>
          <ToggleGroup
            type="single"
            value={timeRange}
            onValueChange={setTimeRange}
            variant="outline"
            className="hidden *:data-[slot=toggle-group-item]:px-4! @[767px]/card:flex"
          >
            <ToggleGroupItem value="90d">Siste 3 mnd</ToggleGroupItem>
            <ToggleGroupItem value="30d">Siste måned</ToggleGroupItem>
            <ToggleGroupItem value="7d">Siste uke</ToggleGroupItem>
          </ToggleGroup>
          <Select value={timeRange} onValueChange={setTimeRange}>
            <SelectTrigger
              className="flex w-40 **:data-[slot=select-value]:block **:data-[slot=selectvalue]:truncate @[767px]/card:hidden"
              size="sm"
              aria-label="Select a value"
            >
              <SelectValue placeholder="Siste 3 mnd" />
            </SelectTrigger>
            <SelectContent className="rounded-xl">
              <SelectItem value="90d" className="rounded-lg">
                Last 3 months
              </SelectItem>
              <SelectItem value="30d" className="rounded-lg">
                Last 30 days
              </SelectItem>
              <SelectItem value="7d" className="rounded-lg">
                Last 7 days
              </SelectItem>
            </SelectContent>
          </Select>
        </CardAction>
      </CardHeader>
      <CardContent className="px-2 pt-4 sm:px-6 sm:pt-6">
        <ChartContainer
          config={chartConfig}
          className="aspect-auto h-62.5 w-full"
        >
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
                  formatter={(value) => `${fmtCurrency(value as number)} brukt`}
                />
              }
            />
            <Area
              dataKey="value"
              type="linear"
              fill="var(--chart-3)"
              stroke="var(--chart-1)"
              stackId="a"
            />
          </AreaChart>
        </ChartContainer>
      </CardContent>
      <CardFooter>
        <div className="flex w-full items-start gap-2 text-sm">
          <div className="grid gap-2">
            <div className="flex items-center gap-2 leading-none font-medium">
              <span>
                {`Din dyreste måned var ${expensiveMonthDate} `}
                <TrendingUp className="h-4 w-4 inline" />
              </span>
            </div>
            <div className="text-muted-foreground flex items-center gap-2 leading-none">
              <span>{`Da brukte du ${fmtCurrency(expensiveMonthTotal)}`}</span>
            </div>
          </div>
        </div>
      </CardFooter>
    </Card>
  );
};
