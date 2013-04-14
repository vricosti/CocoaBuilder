CSCC = cscc
ILASM = ilasm
OUT_FILE=dotgnu.xml.dll

# I had to introduce LIB_FILES to make the order of compilation correct
# and produce a reliable Native.il file ... read pnet's support section
# for bug report on this.

LIB_FILES= Native.cs \
			XmlElementType.cs \
			XmlNode.cs \
			XmlNodeCache.cs \
			XmlNodeFactory.cs \
			XmlNodeList.cs \
			XmlNodeEnumerator.cs \
			XmlElement.cs \
			XmlAttr.cs \
			XmlDoc.cs \
			XmlText.cs \
			XmlPI.cs \
			XmlComment.cs \
			XmlCData.cs \
			XmlNs.cs \
			XmlParser.cs

all : Makefile $(LIB_FILES)
	cscc -ftarget-assembly-name=dotgnu.xml -DDEBUG -shared -S $(LIB_FILES) > err 2>&1
	../tools/pinvoke.sh Native.il
	ilasm -o $(OUT_FILE) Native.il

clean:
	rm -f *.dll *.exe *.*tmp core
	rm -f *.il *~
