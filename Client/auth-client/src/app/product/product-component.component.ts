import { Component, OnInit, ChangeDetectionStrategy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-component',
  standalone: true,
  imports: [CommonModule],
  templateUrl: 'product-component.component.html',
  styleUrls: ['product-component.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductComponent implements OnInit {
  // private readonly service = inject(Service);

  constructor() { }

  ngOnInit(): void {
    // Initialization logic here
  }

}