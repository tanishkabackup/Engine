# Stage 1: Build
FROM node:20-alpine

WORKDIR /app

# Copy package files
COPY package.json package-lock.json ./

# Install ALL dependencies
RUN npm install


COPY . .

# Expose 
EXPOSE 3000

# Start Next.js 
CMD ["npm", "run", "dev"]