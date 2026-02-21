import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem
} from "@/components/shadcn/ui/sidebar";
import { ThemeToggle } from "@components/ui/ThemeToggle";
import { Link } from "react-router";

type Links = {
  route: string;
  label: string;
};

const links: Links[] = [
  { route: "/", label: "Home" },
  { route: "/transactions", label: "Transaksjoner" },
  { route: "/import", label: "Import" },
  { route: "/categories", label: "Kategorier" }
];

export function AppSidebar() {
  return (
    <Sidebar>
      <SidebarHeader />
      <SidebarContent>
        <ThemeToggle />
        <SidebarMenu>
          {links.map((e) => (
            <SidebarMenuItem key={e.route}>
              <SidebarMenuButton asChild>
                <Link to={e.route}>{e.label}</Link>
              </SidebarMenuButton>
            </SidebarMenuItem>
          ))}
        </SidebarMenu>
      </SidebarContent>
      <SidebarFooter />
    </Sidebar>
  );
}
