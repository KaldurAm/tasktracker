import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { issue } from "../models/issue";

@Injectable({
  providedIn: "root",
})
export class IssueService {
  private url: string = "https://localhost:3001/api/issues";
  constructor(private http: HttpClient) {}
  getIssues(pageNumber: number, pageSize: number) {
    return this.http.get<Array<issue>>(
      `${this.url}?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }
}
