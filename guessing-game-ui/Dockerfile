FROM node:18-alpine as builder
WORKDIR /app
COPY guessing-game-ui/package*.json ./
RUN npm install
COPY guessing-game-ui/ .
RUN npm run build

FROM nginx:alpine
COPY --from=builder /app/dist /usr/share/nginx/html
COPY guessing-game-ui/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]