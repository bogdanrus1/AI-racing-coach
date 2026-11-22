<script lang="ts">
  interface Props {
    title: string;
    description?: string;
  }

  let { title, description = "" }: Props = $props();
</script>

<header class="mb-7 flex items-center justify-between">
  <div>
    <h1 class="m-0 text-[26px] font-bold text-[#f3f4f6]">
      {title}
    </h1>

    {#if description}
      <p class="mt-2 text-[13px] text-[#929aa8]">
        {description}
      </p>
    {/if}
  </div>
</header>
