import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService, Product } from '../services/product.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule],
  template: `
<div *ngIf="error">{{ error }}</div>

<ul>
  <li *ngFor="let product of products">
    {{ product.name }} —
    {{ product.price }} —
    {{ product.category.name }} —
    {{ product.stockQuantity }}
  </li>
</ul>

<div *ngIf="!error && products.length === 0">
  No products found
</div>

  `
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  error: string | null = null;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe({
      next: products => (this.products = products),
      error: () => (this.error = 'Failed to load products')
    });
  }
}
