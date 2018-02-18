# Rename-Photos

A small command line utility to rename photos by their property `Date Taken`. 

## How it works

The utility just will search for `.jpg` files, extract their property called `Date Taken` and will rename the file.  

Plus, it would add a `0000` sufix, in order that, if your have more than one picture taken at the same time, you don't have duplicated names.

### Output

> 20180212-091102-0001.jpg  
> 20180212-091102-0002.jpg  
> 20180212-091106-0003.jpg

## Usage

> Carneiro.RenamePhotos.exe [path]

### Parameters

* **Path**: Sets the path where the `jpg` are. If not set, the current folder is used. *(optional)*

