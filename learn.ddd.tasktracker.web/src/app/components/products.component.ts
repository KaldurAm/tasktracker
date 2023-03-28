import { Component, OnInit } from "@angular/core";
import { product } from "src/app/models/product";
import { ProductService } from "src/app/services/product.service";
import { productWithDetails as productWithDetails } from "../models/productWithDetails";

@Component({
  selector: "app-products",
  styles: [
    `
      .product__information__container {
        margin-top: 10px;
        margin-left: 10px;
        margin-right: 10px;
        display: flex;
      }
    `,
    `
      .product__list__container {
        margin-right: 2px;
        width: 50%;
      }
    `,
    `
      .product__details__container {
        margin-left: 2px;
        width: 50%;
      }
    `,
    `
      td {
        cursor: pointer;
      }
    `,
    `
      .member__content {
        margin-bottom: 5px;
      }
    `,
    `
      .issue__card {
        margin-bottom: 5px;
      }
    `,
    `
      .issue__card__content {
        display: flext;
      }
    `,
    `
      .mat-mdc-row .mat-mdc-cell {
        border-bottom: 1px solid transparent;
        border-top: 1px solid transparent;
        cursor: pointer;
      }

      .mat-mdc-row:hover .mat-mdc-cell {
        border-color: grey;
      }

      .demo-row-is-clicked {
        font-weight: bold;
      }
    `,
  ],
  template: ` <div class="product__information__container">
    <div class="product__list__container">
      <table mat-table [dataSource]="products$" class="mat-elevation-z8">
        <ng-container matColumnDef="name">
          <th mat-header-cell *matHeaderCellDef>Name</th>
          <td mat-cell *matCellDef="let element">{{ element.name }}</td>
        </ng-container>

        <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
        <tr
          mat-row
          (click)="openDetails(row.id)"
          *matRowDef="let row; columns: displayedColumns"
        ></tr>
      </table>
    </div>
    <div *ngIf="productDetail$" class="product__details__container">
      <mat-card>
        <mat-card-content>
          <mat-card-title>{{ productDetail$.name }}</mat-card-title>
          <mat-card-subtitle>{{
            productDetail$.description
          }}</mat-card-subtitle>
          <mat-divider></mat-divider>
          <mat-accordion>
            <mat-expansion-panel hideToggle>
              <mat-expansion-panel-header>
                <mat-panel-title>
                  {{ productDetail$.backlog.name }}
                </mat-panel-title>
                <mat-panel-description>
                  count of issues: {{ productDetail$.backlog.issues.length }}
                </mat-panel-description>
              </mat-expansion-panel-header>
              <mat-card
                class="issue__card"
                *ngFor="let issue of productDetail$.backlog.issues"
              >
                <mat-card-content class="issue__card__content">
                  <div class="issue__card__content__left">
                    <mat-card-title>{{ issue.title }}</mat-card-title>
                    <mat-card-subtitle>{{
                      issue.description
                    }}</mat-card-subtitle>
                  </div>

                  <div class="issue__card__content__right">
                    <mat-card-title>Estimation</mat-card-title>
                    <mat-card-subtitle>{{
                      issue.estimation
                    }}</mat-card-subtitle>
                  </div>
                </mat-card-content>
              </mat-card>
            </mat-expansion-panel>
            <mat-expansion-panel hideToggle>
              <mat-expansion-panel-header>
                <mat-panel-title>
                  {{ productDetail$.team.name }}
                </mat-panel-title>
                <mat-panel-description>
                  count of members: {{ productDetail$.team.members.length }}
                </mat-panel-description>
              </mat-expansion-panel-header>
              <mat-card
                class="member__content"
                *ngFor="let member of productDetail$.team.members"
              >
                <mat-card-content
                  ><mat-card-title>
                    {{ member.lastName }}
                    {{ member.firstName }}
                    ({{ member.alias }})</mat-card-title
                  >
                  <mat-card-subtitle>{{ member.email }}</mat-card-subtitle>
                </mat-card-content>
              </mat-card>
            </mat-expansion-panel>
          </mat-accordion>
        </mat-card-content>
      </mat-card>
    </div>
  </div>`,
})
export class ProductsComponent implements OnInit {
  panelOpenState: boolean = true;
  products$: Array<product> = [];
  productDetail$: productWithDetails = null!;
  displayedColumns: string[] = ["name"];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.getProducts(1, 10).subscribe((response) => {
      if (response) {
        this.products$ = response;
      }
    });
  }

  openDetails(id: string): void {
    this.productService.getProductDetails(id).subscribe((response) => {
      if (response) this.productDetail$ = response;
    });
  }
}
