import { Component, OnInit } from "@angular/core";
import { issue } from "../models/issue";

@Component({
  selector: "app-issues",
  styles: [``],
  template: `<div class="issues__list__container"></div>`,
})
export class IssuesComponent implements OnInit {
  issues: issue[] = new Array<issue>();

  constructor(private issueService: IssueService) {}

  ngOnInit(): void {}

  openDetails(id: string): void {}
}
