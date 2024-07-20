BINARY_CFFI_TEST=cffi-test
BINARY_READ_TABLE_TEST=examples/read-table/read_table

SOURCES_CFFI_TEST=cffi-test.c
SOURCES_READ_TABLE_TEST=examples/read-table/read_table.c

PATHS_INCLUDE=../target/ffi-headers
PATHS_LIB=../target/debug

LDFLAGS=-ldelta_kernel_ffi #`pkg-config --libs arrow-glib`
CFLAGS=-c -g -Wall -DDEFINE_DEFAULT_ENGINE -DDEFINE_SYNC_ENGINE #`pkg-config --cflags arrow-glib`

CC=gcc

CFFITESTOBJECTS=$(SOURCES_CFFI_TEST:.c=.o)
READOBJECTS=$(SOURCES_READ_TABLE_TEST:.c=.o)

INCFLAGS=$(foreach TMP,$(PATHS_INCLUDE),-I$(TMP))
LIBFLAGS=$(foreach TMP,$(PATHS_LIB),-L$(TMP))

ROOT_DIR:=$(shell dirname $(realpath $(firstword $(MAKEFILE_LIST))))

all: $(SOURCES_CFFI_TEST) $(SOURCES_READ_TABLE_TEST) $(BINARY_CFFI_TEST) $(BINARY_READ_TABLE_TEST)

$(BINARY_CFFI_TEST): $(CFFITESTOBJECTS)
	$(CC) $(LIBFLAGS) $(CFFITESTOBJECTS) $(LDFLAGS) -o $@

$(BINARY_READ_TABLE_TEST): $(READOBJECTS)
	$(CC) $(LIBFLAGS) $(READOBJECTS) $(LDFLAGS) -o $@

.c.o:
	$(CC) $(INCFLAGS) $(CFLAGS) -fPIC $< -o $@

run: $(BINARY_CFFI_TEST)
	LD_LIBRARY_PATH=$(PATHS_LIB) ./$(BINARY_CFFI_TEST) $(table)

test: $(BINARY_CFFI_TEST)
	LD_LIBRARY_PATH=$(PATHS_LIB) ./$(BINARY_CFFI_TEST) file://$(ROOT_DIR)/../kernel/tests/data/basic_partitioned

distclean: clean
	rm -f $(BINARY_CFFI_TEST) $(BINARY_READ_TABLE_TEST)

clean:
	rm -f $(CFFITESTOBJECTS) $(READOBJECTS) $(BINARY_CFFI_TEST) $(BINARY_READ_TABLE_TEST)
