import { SidebarTrigger } from "../shadcn/ui/sidebar";
import { ThemeToggle } from "../ui/ThemeToggle";

export const SiteHeader = () => (
  <header>
    <div className="flex w-full items-center gap-1 px-4 lg:gap-2 lg:px-6">
      <SidebarTrigger />
      <ThemeToggle />
    </div>
  </header>
);
