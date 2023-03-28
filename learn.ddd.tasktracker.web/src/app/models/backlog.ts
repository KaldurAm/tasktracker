import { issue } from "./issue";

export interface backlog {
  id: string;
  name: string;
  productId: string;
  issues: issue[];
}
