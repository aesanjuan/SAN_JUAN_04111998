import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { UploadFormComponent } from './components/upload-form/upload-form.component';
import { HomeComponent } from './components/home/home.component';
import { StreamComponent } from './components/stream/stream/stream.component';

export const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: 'home' },
    { path: 'home', component: HomeComponent },
    { path: 'upload', component: UploadFormComponent },
    { path: 'stream/:id', component: StreamComponent, data: { id: '' } },
    { path: '**', component: HomeComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes), HttpClientModule],
    exports: [RouterModule]
})

export class AppRoutingModule {}