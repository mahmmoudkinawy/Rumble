import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MaterialModule } from './shared/material.module';
import { HomeComponent } from './pages/home/home.component';
import { RegisterComponent } from './pages/register/register.component';
import { MemberDetailComponent } from './pages/members/member-detail/member-detail.component';
import { MemberListComponent } from './pages/members/member-list/member-list.component';
import { MessagesComponent } from './pages/messages/messages.component';
import { ListsComponent } from './pages/lists/lists.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberCardComponent } from './pages/members/member-card/member-card.component';
import { MemberEditComponent } from './pages/member-edit/member-edit.component';

import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { ErrorsInterceptor } from './interceptors/errors.interceptor';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { PhotoEditorComponent } from './pages/members/photo-editor/photo-editor.component';
import { InputTextComponent } from './forms/input-text/input-text.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
    MemberDetailComponent,
    MemberListComponent,
    MessagesComponent,
    ListsComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MemberCardComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    InputTextComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorsInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
