import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FlexLayoutModule } from '@angular/flex-layout';

import { MatSliderModule } from '@angular/material/slider';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';

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
  MatSnackBarModule,
  MatCheckboxModule,
  MatProgressSpinnerModule,
  MatGridListModule,
  MatIconModule,
];

@NgModule({
  imports: [CommonModule, MaterialComponents, FlexLayoutModule],
  exports: [MaterialComponents, FlexLayoutModule],
})
export class MaterialModule {}
