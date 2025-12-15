-- seed categories
insert into categories (id, name, created_at, updated_at)
values
  ('11111111-1111-1111-1111-111111111111', 'Food', now(), now()),
  ('22222222-2222-2222-2222-222222222222', 'Books', now(), now()),
  ('33333333-3333-3333-3333-333333333333', 'Electronics', now(), now()),
  ('44444444-4444-4444-4444-444444444444', 'Clothing', now(), now()),
  ('55555555-5555-5555-5555-555555555555', 'Home', now(), now());

-- seed products: 25 per category
insert into products (id, category_id, name, price, created_at, updated_at)
select
  gen_random_uuid(),
  c.id,
  c.name || ' Product ' || g.n,
  round((random() * 90 + 10)::numeric, 2),
  now(),
  now()
from categories c
cross join generate_series(1, 25) as g(n);
