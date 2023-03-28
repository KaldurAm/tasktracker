import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { product } from "../models/product";
import { productWithDetails } from "../models/productWithDetails";

@Injectable({
  providedIn: "root",
})
export class ProductService {
  private url: string = "https://localhost:3001/api/products";

  constructor(private http: HttpClient) {}

  getProducts(pageNumber: number = 1, pageSize: number = 10) {
    return this.http.get<Array<product>>(
      `${this.url}?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  getProductDetails(id: string) {
    return this.http.get<productWithDetails>(this.url + "/details/" + id);
  }
}
