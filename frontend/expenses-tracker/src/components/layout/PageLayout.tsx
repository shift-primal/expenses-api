import {
  SidebarInset,
  SidebarProvider,
  SidebarTrigger
} from "../shadcn/ui/sidebar";
import { ThemeProvider } from "../ui/theme-provider";
import { AppSidebar } from "./AppSidebar";
import { SiteHeader } from "./SiteHeader";

export const PageLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <ThemeProvider>
      <SidebarProvider>
        <AppSidebar />
        <SiteHeader />
        <main className="min-h-screen w-full flex">{children}</main>
      </SidebarProvider>
    </ThemeProvider>
  );
};
