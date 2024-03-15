-include .env
NAME = plataformacliente
REPO = bempromotora/$(NAME)
IMAGETAG=latest

print:
	@echo "NAME: $(NAME)"
	@echo "REPO: $(REPO)"

build-image:
	docker build . -t $(REPO):$(IMAGETAG)

push-image:
	docker push $(REPO):$(IMAGETAG)

run:
	docker run -p 1433:1433 -d $(REPO):$(IMAGETAG)