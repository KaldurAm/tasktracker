import { backlog } from "./backlog";
import { team } from "./team";

export interface productWithDetails {
  id: string;
  name: string;
  description: string;
  team: team;
  backlog: backlog;
}
