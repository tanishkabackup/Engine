# Use the high-performance alpine version
FROM redis:7.4-alpine

# Set working directory for potential future config files
WORKDIR /usr/local/etc/redis

# Start Redis with Persistence and Password using environment variable expansion
CMD redis-server --appendonly yes --requirepass "$REDIS_PASSWORD"