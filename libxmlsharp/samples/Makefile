CSCC=cscc
ILRUN=ilrun

all: Makefile dotgnu.xml.dll libxml_wrapper.so build

build: tree.exe struct.exe builder.exe ns.exe

.SUFFIXES: .cs .exe
.cs.exe:
	$(CSCC) -o $@ -ldotgnu.xml.dll $<

dotgnu.xml.dll: Makefile ../dotgnu.xml/dotgnu.xml.dll
	ln -sf ../dotgnu.xml/dotgnu.xml.dll .

libxml_wrapper.so: Makefile ../wrapper/libxml_wrapper.so
	ln -sf ../wrapper/libxml_wrapper.so .
	
clean: Makefile
	rm -rf tree.exe struct.exe builder.exe
	rm -rf dotgnu.xml.dll libxml_wrapper.so
