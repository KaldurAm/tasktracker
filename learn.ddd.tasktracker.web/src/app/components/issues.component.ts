import { Component, OnInit } from "@angular/core";
import { issue } from "../models/issue";
import { IssueService } from "../services/issue.service";

@Component({
  selector: "app-issues",
  styles: [
    `
      .issues-container {
        width: 100%;
        max-width: 800px;
        margin: 0 auto;
      }

      table {
        width: 100%;
        border-collapse: collapse;
      }

      th,
      td {
        padding: 10px;
        border: 1px solid #ccc;
      }

      thead {
        background-color: #f5f5f5;
      }

      tr:nth-child(even) {
        background-color: #f2f2f2;
      }
    `,
  ],
  template: `<div class="issues-container">
    <table>
      <thead>
        <tr>
          <th>Title</th>
          <th>Description</th>
        </tr>
      </thead>
      <tbody>
        <tr *ngFor="let issue of this.issues">
          <td>{{ issue.title }}</td>
          <td>{{ issue.description }}</td>
        </tr>
      </tbody>
    </table>
  </div>`,
})
export class IssuesComponent implements OnInit {
  issues: Array<issue> = new Array<issue>();
  pageNumber: number = 1;
  pageSize: number = 10;

  constructor(private issueService: IssueService) {}

  ngOnInit(): void {
    this.issueService
      .getIssues(this.pageNumber, this.pageSize)
      .subscribe((response) => {
        console.log(response);
        if (response) this.issues = response;
      });
  }

  openDetails(id: string): void {}
}
