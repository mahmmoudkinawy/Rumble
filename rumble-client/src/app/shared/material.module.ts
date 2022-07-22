import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatSliderModule } from '@angular/material/slider';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';

const MaterialComponents = [
  MatSliderModule,
  MatSlideToggleModule,
  MatToolbarModule,
  MatProgressBarModule,
  MatButtonModule,
  MatCardModule,
  MatInputModule,
  MatSelectModule,
  MatDividerModule,
];

@NgModule({
  imports: [CommonModule, MaterialComponents],
  exports: [MaterialComponents],
})
export class MaterialModule {}
