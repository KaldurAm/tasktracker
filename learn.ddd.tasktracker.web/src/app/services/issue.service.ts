import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root",
})
export class IssueService {
  private url: string = "https://localhost:3001/api/issues";
  constructor(private http: HttpClient) {}
  getIssues() {}
}
