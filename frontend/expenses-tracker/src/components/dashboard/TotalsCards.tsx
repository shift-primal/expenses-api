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
      color: "bg-green-300"
    }
  },
  expenses: {
    label: "Ut fra konto",
    icon: {
      component: <Minus />,
      color: "bg-red-300"
    }
  },
  balance: {
    label: "Total balanse",
    icon: {
      component: <Equal />,
      color: "bg-yellow-300"
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
  <Card className="w-full">
    <CardHeader>
      <CardDescription>{cardData[type].label}</CardDescription>
      <CardTitle className="text-2xl font-semibold tabular-nums flex">
        {value}
      </CardTitle>
      <CardAction>
        <Badge variant="outline" className={cardData[type].icon.color}>
          {cardData[type].icon.component}
        </Badge>
      </CardAction>
    </CardHeader>
    <CardFooter className="flex col items-start gap-1.5 text-sm">
      <div className="line-clamp-1 flex gap-2 font-medium">Footer</div>
    </CardFooter>
  </Card>
);

export const TotalsSummary = ({ data }: { data: Totals }) => (
  <div className="flex gap-4 grid-rows-3">
    {Object.entries(data).map(([type, value]) => (
      <TotalsCard
        key={type}
        type={type as keyof typeof cardData}
        value={value as number}
      />
    ))}
  </div>
);
