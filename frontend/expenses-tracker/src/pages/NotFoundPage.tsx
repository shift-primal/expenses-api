import { PageLayout } from "@layout/PageLayout";
import { Link } from "react-router";

export const NotFoundPage = () => {
  return (
    <PageLayout>
      <div className="flex flex-1 flex-col gap-4 p-4">
        404 Not Found
        <Link to="/">Tilbake</Link>
      </div>
    </PageLayout>
  );
};
