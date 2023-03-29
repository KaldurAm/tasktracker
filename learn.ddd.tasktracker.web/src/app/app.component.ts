import { Component } from "@angular/core";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent {
  title = "tasktracker";
  navigationPage: number = 1;
  myDate: Date = new Date();

  navigateTo(page: number) {
    this.navigationPage = page;
  }

  onDateTimeSelected(dateTime: Date) {
    console.log("Selected date and time:", dateTime);
  }
}
