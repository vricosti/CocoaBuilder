CC = gcc
INCLUDES=`gnome-config --cflags xml2` -DDEBUG
LIB_OPTS=-shared -fPIC -g -O2 `gnome-config --libs xml2`
LIB_FILE=libxml_wrapper.o
OUT_FILE=libxml_wrapper.so

.SUFFIXES: .c .o

all: Makefile $(LIB_FILE)
	$(CC) $(LIB_OPTS) $(LIB_FILE) -o $(OUT_FILE)
.c.o:
	gcc $(INCLUDES) $< -c -o $@
clean: Makefile $(OUT_FILE)
	rm -rf $(OUT_FILE)
