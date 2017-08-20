import { ImageCropperComponent, ImageCropperModule } from 'ng2-img-cropper';
import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";

@NgModule({
imports:[
    CommonModule
],
declarations: [
    ImageCropperComponent
],
exports:[
    ImageCropperComponent
]
})
export class SharedModule { }