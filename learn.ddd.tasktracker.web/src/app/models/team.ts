import { member } from "./member";

export interface team {
  id: string;
  name: string;
  productId: string;
  members: member[];
}
