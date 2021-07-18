// Angular Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

// flatpickr module
import { FlatpickrModule } from 'angularx-flatpickr';

// PrimeNG modules
import { CheckboxModule } from 'primeng/checkbox';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { InputSwitchModule } from 'primeng/inputswitch';
import { MultiSelectModule } from 'primeng/multiselect';
import { SpinnerModule } from 'primeng/spinner';
import { TabViewModule } from 'primeng/tabview';
import { DialogModule } from 'primeng/dialog';
import { InputMaskModule } from 'primeng/InputMask';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { TooltipModule as pTooltipModule } from 'primeng/tooltip';
import { ProgressBarModule } from 'primeng/progressbar';
import { AccordionModule } from 'primeng/accordion';
import { SelectButtonModule } from 'primeng/selectbutton';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { SidebarModule } from 'primeng/sidebar';
import { DataViewModule } from 'primeng/dataview';
import { StepsModule } from 'primeng/steps';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { PaginatorModule } from 'primeng/paginator';
import { CardModule } from 'primeng/card';
import { NgSelectModule } from '@ng-select/ng-select';
import { SplitButtonModule } from 'primeng/splitbutton';
import { ToolbarModule } from 'primeng/toolbar';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

// AmChart module
import { AmChartsModule } from '@amcharts/amcharts3-angular';
import { SafePercentPipe } from './pipes/safe.percent.pipe';
import { SafeNumberPipe } from './pipes/safe.number.pipe';

import { TranslateModule } from '@ngx-translate/core';

const primengModules = [
  CheckboxModule,
  CalendarModule,
  DropdownModule,
  MultiSelectModule,
  TableModule,
  InputSwitchModule,
  SpinnerModule,
  TabViewModule,
  DialogModule,
  InputMaskModule,
  AutoCompleteModule,
  OverlayPanelModule,
  pTooltipModule,
  ProgressBarModule,
  AccordionModule,
  SelectButtonModule,
  SidebarModule,
  StepsModule,
  ToggleButtonModule,
  DataViewModule,
  ScrollPanelModule,
  PaginatorModule,
  CardModule,
  SplitButtonModule,
  ToolbarModule,
  ConfirmDialogModule,
];

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule,
    AmChartsModule,
    ...primengModules,
    NgSelectModule,
    FlatpickrModule,
    TranslateModule,
  ],
  providers: [SafePercentPipe, SafeNumberPipe, ConfirmationService],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    AmChartsModule,
    ...primengModules,
    NgSelectModule,
    SafePercentPipe,
    SafeNumberPipe,
    TranslateModule,
  ],
  declarations: [SafePercentPipe, SafeNumberPipe],
})
export class SharedModule {}
