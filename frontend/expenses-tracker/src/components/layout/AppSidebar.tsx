import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem
} from "@/components/shadcn/ui/sidebar";
import { Link } from "react-router";

const links: { route: string; label: string }[] = [
  { route: "/", label: "Home" },
  { route: "/transactions", label: "Transaksjoner" },
  { route: "/import", label: "Import" },
  { route: "/categories", label: "Kategorier" }
];

export const AppSidebar = () => {
  return (
    <Sidebar collapsible="offcanvas">
      <SidebarHeader />
      <SidebarContent>
        <SidebarMenu>
          <span className="m-4 text-base font-semibold">Waste Tracker</span>
          {links.map((e) => (
            <SidebarMenuItem key={e.route}>
              <SidebarMenuButton asChild>
                <Link to={e.route} className="mx-4">
                  {e.label}
                </Link>
              </SidebarMenuButton>
            </SidebarMenuItem>
          ))}
        </SidebarMenu>
      </SidebarContent>
      <SidebarFooter />
    </Sidebar>
  );
};
