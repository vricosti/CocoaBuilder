# libtidy

LibTidy is a library to clean or process HTML documents, well formed or not.
For more information, please see tidy [homepage](http://tidy.sf.net).

## Available prebuilt libraries

All prebuilt libraries are available in the php-libs
[repositories](http://windows.php.net/downloads/)

The filename format is:

libtidy-x.y.z-vcversion-architecture.zip

where x.y.z defines the version, vcversion which Visual C++ has been used and
architecture defines whether the build is for win32 or win64.

## Building LibTidy

### Requirements

  * tidy sources (2008/03/22), fetch our [version](https://github.com/winlibs/libtidy) or the original [sources](http://tidy.sf.net) (CVS only) and use the Makefiles provided in our sources.

  * Common tools used to compile PHP

### Preparing the sources

Simply uncompress the sources archives.

### Configuration

No special configuration required. Be sure to have the platform SDK in your
path.

### Compilation

#### Release

The following command:

    
    cd tidy\build\msvc
    nmake /f makeLIB.vc
    
    cd tidy\build\msvc
    nmake /f makeDLL.vc

Debug mode:

    
    cd tidy\build\msvc
    nmake /f makeLIB.vc DEBUG=1