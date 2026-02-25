import {
  Card,
  CardAction,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle
} from "@/components/shadcn/ui/card";
import type { Totals } from "@/types";
import { Equal, Minus, Plus } from "lucide-react";
import { Badge } from "../shadcn/ui/badge";

const cardData = {
  income: {
    label: "Inn på konto",
    icon: {
      component: <Plus />,
      color: "bg-green-400"
    }
  },
  expenses: {
    label: "Ut fra konto",
    icon: {
      component: <Minus />,
      color: "bg-red-400"
    }
  },
  balance: {
    label: "Total balanse",
    icon: {
      component: <Equal />,
      color: "bg-yellow-400"
    }
  },
  placeholder: {
    label: "Røyk",
    icon: {
      component: <Equal />,
      color: "bg-yellow-purple-400"
    }
  }
};

const TotalsCard = ({
  type,
  value
}: {
  type: keyof typeof cardData;
  value: number;
}) => (
  <Card className="@container/card">
    <CardHeader>
      <CardDescription>{cardData[type].label}</CardDescription>
      <CardTitle className="text-2xl font-semibold tabular-nums @[250px]/card:text-3xl">
        {value}
      </CardTitle>
      <CardAction>
        <Badge variant="outline" className={cardData[type].icon.color}>
          {cardData[type].icon.component}
        </Badge>
      </CardAction>
    </CardHeader>

    <CardFooter className="flex-col items-start gap-1.5 text-sm">
      <div className="line-clamp-1 flex gap-2 font-medium">
        Trending up this month
      </div>
      <div className="text-muted-foreground">Hey</div>
    </CardFooter>
  </Card>
);

export const TotalsSummary = ({ data }: { data: Totals }) => (
  <div className="grid grid-cols-1 gap-4 px-4 *:data-[slot=card]:bg-linear-to-t *:data-[slot=card]:shadow-xs lg:px-6 @xl/main:grid-cols-2 @5xl/main:grid-cols-4">
    {Object.entries(data).map(([type, value]) => (
      <TotalsCard
        key={type}
        type={type as keyof typeof cardData}
        value={value as number}
      />
    ))}
  </div>
);
