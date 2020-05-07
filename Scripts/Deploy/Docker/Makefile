build :
	./build.sh

debug :
	docker run -it --entrypoint=/bin/bash --net=host --privileged -v $(shell pwd)/override/appsettings.json:/Birdy/appsettings.json -v $(shell pwd)/override/entrypoint.sh:/Birdy/entrypoint.sh birdy

run :
	docker run -d --net=host --privileged -v $(shell pwd)/override/appsettings.json:/Birdy/appsettings.json -v $(shell pwd)/override/entrypoint.sh:/Birdy/entrypoint.sh --name=birdy_c birdy

clean :
	docker kill birdy_c || docker rm birdy_c