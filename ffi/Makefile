CFFITESTBINARY=cffi-test
READBINARY=examples/read-table/read_table

SOURCES=cffi-test.c
READSOURCES=examples/read-table/read_table.c

INCPATHS=../target/ffi-headers
LIBPATHS=../target/debug

LDFLAGS=-ldelta_kernel_ffi #`pkg-config --libs arrow-glib`
CFLAGS=-c -g -Wall -DDEFINE_DEFAULT_ENGINE -DDEFINE_SYNC_ENGINE #`pkg-config --cflags arrow-glib`

CC=gcc

OBJECTS=$(SOURCES:.c=.o)
READOBJECTS=$(READSOURCES:.c=.o)

INCFLAGS=$(foreach TMP,$(INCPATHS),-I$(TMP))
LIBFLAGS=$(foreach TMP,$(LIBPATHS),-L$(TMP))

ROOT_DIR:=$(shell dirname $(realpath $(firstword $(MAKEFILE_LIST))))

all: $(SOURCES) $(READSOURCES) $(CFFITESTBINARY) $(READBINARY)

$(CFFITESTBINARY): $(OBJECTS)
	$(CC) $(LIBFLAGS) $(OBJECTS) $(LDFLAGS) -o $@

$(READBINARY): $(READOBJECTS)
	$(CC) $(LIBFLAGS) $(READOBJECTS) $(LDFLAGS) -o $@

.c.o:
	$(CC) $(INCFLAGS) $(CFLAGS) -fPIC $< -o $@

run: $(CFFITESTBINARY)
	LD_LIBRARY_PATH=$(LIBPATHS) ./$(CFFITESTBINARY) $(table)

test: $(CFFITESTBINARY)
	LD_LIBRARY_PATH=$(LIBPATHS) ./$(CFFITESTBINARY) file://$(ROOT_DIR)/../kernel/tests/data/basic_partitioned

distclean: clean
	rm -f $(CFFITESTBINARY) $(READBINARY)

clean:
	rm -f $(OBJECTS) $(READOBJECTS) $(CFFITESTBINARY) $(READBINARY)
