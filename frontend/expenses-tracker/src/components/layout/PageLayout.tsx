import { SidebarProvider, SidebarTrigger } from "../shadcn/ui/sidebar";
import { ThemeProvider } from "../ui/theme-provider";
import { AppSidebar } from "./AppSidebar";

export const PageLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <ThemeProvider>
      <SidebarProvider>
        <AppSidebar />
        <main className="min-h-screen w-full flex">
          <SidebarTrigger />
          {children}
        </main>
      </SidebarProvider>
    </ThemeProvider>
  );
};
