all : Makefile Wrapper Csharp Test
	
Wrapper: Makefile
	cd wrapper && make && cd ..
	
Wrapper-clean: Makefile
	cd wrapper && make clean && cd ..

Csharp: Makefile
	cd dotgnu.xml && make && cd ..

Csharp-clean: Makefile
	cd dotgnu.xml && make clean && cd ..

Test: Makefile
	cd samples && make && cd ..

Test-clean: Makefile
	cd samples && make clean && cd ..

install:
	@#cp -f libxml_wrapper.so /usr/local/lib/
	@#cp -f dotgnu.xml.dll /usr/local/lib/cscc/lib/
	@echo "not yet ... we'll fix INSTALL later"

clean: Makefile Wrapper-clean Csharp-clean Test-clean
