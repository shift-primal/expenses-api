import { TrendingUp } from "lucide-react";
import { Pie, PieChart } from "recharts";

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
import type { CategorySummary, MonthSummary } from "@types";
import { getDateRange, getMostExpensiveCategory } from "@lib/chartUtils";
import { useIsMobile } from "@hooks/useIsMobile";
import { useEffect, useState } from "react";
import {
  ToggleGroup,
  ToggleGroupItem
} from "@components/shadcn/ui/toggle-group";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from "@components/shadcn/ui/select";

export const PieByCategoryChart = ({
  categoryData,
  monthData
}: {
  categoryData: CategorySummary[];
  monthData: MonthSummary[];
}) => {
  const chartData = categoryData.map((c, i) => ({
    name: c.category.name,
    value: Math.abs(c.total),
    fill: `var(--chart-${(i % 5) + 1})`
  }));

  const chartConfig = Object.fromEntries(
    categoryData.map((c) => [c.category.name, { label: c.category.name }])
  ) satisfies ChartConfig;

  const {
    mostExpensiveCategoryName,
    mostExpensiveCategoryTotal,
    mostExpensiveCategoryCount
  } = getMostExpensiveCategory(categoryData);

  const { min, max, diffInDays } = getDateRange(monthData);

  const isMobile = useIsMobile();
  const [timeRange, setTimeRange] = useState({
    min,
    max,
    range: `${diffInDays}d`
  });

  useEffect(() => {
    if (isMobile) {
      setTimeRange((p) => {
        return { ...p, range: "7d" };
      });
    }
  }, [isMobile]);

  return (
    <Card className="@container/card">
      <CardHeader>
        <CardTitle>Kategori</CardTitle>
        <CardDescription>
          <span>
            {min} - {max}
          </span>
        </CardDescription>
        <CardAction>
          <ToggleGroup
            type="single"
            value={timeRange.range}
            onValueChange={(v) =>
              setTimeRange((p) => {
                return { ...p, range: v };
              })
            }
            variant="outline"
            className="hidden *:data-[slot=toggle-group-item]:px-4! @[767px]/card:flex"
          >
            <ToggleGroupItem value="90d">Siste 3 mnd</ToggleGroupItem>
            <ToggleGroupItem value="30d">Siste måned</ToggleGroupItem>
            <ToggleGroupItem value="7d">Siste uke</ToggleGroupItem>
          </ToggleGroup>
          <Select
            value={timeRange.range}
            onValueChange={(v) =>
              setTimeRange((p) => {
                return { ...p, range: v };
              })
            }
          >
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
          <PieChart
            accessibilityLayer
            data={chartData}
            margin={{
              left: 12,
              right: 12
            }}
          >
            <ChartTooltip
              cursor={false}
              content={<ChartTooltipContent hideLabel />}
            />
            <Pie data={chartData} dataKey="value" nameKey="name" />
          </PieChart>
        </ChartContainer>
      </CardContent>
      <CardFooter>
        <div className="flex w-full items-start gap-2 text-sm">
          <div className="grid gap-2">
            <div className="flex items-center gap-2 leading-none font-medium">
              <span>
                {`Du brukte mest penger på ${mostExpensiveCategoryName} `}
                <TrendingUp className="h-4 w-4 inline" />
              </span>
            </div>
            <div className="text-muted-foreground flex items-center gap-2 leading-none">
              <span>
                {`Over ${mostExpensiveCategoryCount} transaksjoner, brukte du
            ${mostExpensiveCategoryTotal}kr`}
              </span>
            </div>
          </div>
        </div>
      </CardFooter>
    </Card>
  );
};
