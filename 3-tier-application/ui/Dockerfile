FROM nginx:alpine

# We'll need 'sed' for the runtime substitution
RUN apk add --no-cache bash

WORKDIR /usr/share/nginx/html

# Copy all UI code
COPY . .

# Copy an entrypoint script
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

EXPOSE 80
ENTRYPOINT ["/entrypoint.sh"]
CMD ["nginx", "-g", "daemon off;"]
