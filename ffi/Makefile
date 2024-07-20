# Source of truth, changing this triggers rebuild
#
SOURCES_CFFI_TEST=cffi-test.c
SOURCES_READ_TABLE_TEST=examples/read-table/read_table.c

# The binaries that get executed
#
BINARY_CFFI_TEST=cffi-test
BINARY_READ_TABLE_TEST=examples/read-table/read_table

# Various paths to include during build
#
PATHS_INCLUDE=../target/ffi-headers
PATHS_LIB=../target/debug

# Objects have the same name as the sources, but with .o extension
#
OBJECTS_CFFI_TEST=$(SOURCES_CFFI_TEST:.c=.o)
OBJECTS_READ_TABLE_TEST=$(SOURCES_READ_TABLE_TEST:.c=.o)

# Compiler flags
#
FLAGS_COMPILER=-c -g -Wall -DDEFINE_DEFAULT_ENGINE -DDEFINE_SYNC_ENGINE `pkg-config --cflags arrow-glib`
FLAGS_INCLUDE=$(foreach TMP,$(PATHS_INCLUDE),-I$(TMP))
FLAGS_LIB=$(foreach TMP,$(PATHS_LIB),-L$(TMP))
FLAGS_LINK_DELTA_FFI=-ldelta_kernel_ffi `pkg-config --libs arrow-glib`

COMPILER=gcc

ROOT_DIR:=$(shell dirname $(realpath $(firstword $(MAKEFILE_LIST))))

all: $(SOURCES_CFFI_TEST) $(SOURCES_READ_TABLE_TEST) $(BINARY_CFFI_TEST) $(BINARY_READ_TABLE_TEST)

$(BINARY_CFFI_TEST): $(OBJECTS_CFFI_TEST)
	$(COMPILER) $(FLAGS_LIB) $(OBJECTS_CFFI_TEST) $(FLAGS_LINK_DELTA_FFI) -o $@

$(BINARY_READ_TABLE_TEST): $(OBJECTS_READ_TABLE_TEST)
	$(COMPILER) $(FLAGS_LIB) $(OBJECTS_READ_TABLE_TEST) $(FLAGS_LINK_DELTA_FFI) -o $@

# Tells make how to transform a .c file into a .o file
#
.c.o:
	$(COMPILER) $(FLAGS_INCLUDE) $(FLAGS_COMPILER) -fPIC $< -o $@

run: $(BINARY_CFFI_TEST)
	LD_LIBRARY_PATH=$(PATHS_LIB) ./$(BINARY_CFFI_TEST) $(table)

test: $(BINARY_CFFI_TEST)
	LD_LIBRARY_PATH=$(PATHS_LIB) ./$(BINARY_CFFI_TEST) file://$(ROOT_DIR)/../kernel/tests/data/basic_partitioned

distclean: clean
	rm -f $(BINARY_CFFI_TEST) $(BINARY_READ_TABLE_TEST)

clean:
	rm -f $(OBJECTS_CFFI_TEST) $(OBJECTS_READ_TABLE_TEST) $(BINARY_CFFI_TEST) $(BINARY_READ_TABLE_TEST)
